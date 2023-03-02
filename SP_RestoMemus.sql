﻿CREATE TRIGGER trg_update_order_menus_total_item
ON Resto.order_menu_detail
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @orme_id int;
    SET @orme_id = (SELECT DISTINCT omde_orme_id FROM inserted);
    IF @orme_id IS NOT NULL
    BEGIN
        UPDATE Resto.order_menus
        SET orme_total_item = (SELECT SUM(orme_qty) FROM Resto.order_menu_detail WHERE omde_orme_id = @orme_id),
            orme_total_discount = (SELECT SUM(orme_discount) FROM Resto.order_menu_detail WHERE omde_orme_id = @orme_id),
            orme_total_amount = (SELECT SUM(orme_subtotal) - SUM(orme_discount)  FROM Resto.order_menu_detail WHERE omde_orme_id = @orme_id),
            orme_modified_date = GETDATE()
        WHERE orme_id = @orme_id;
    END
END
go

drop procedure  Resto.create_order_menu_detail;

--SP BARU

CREATE PROCEDURE create_order_menu_detail
  @omde_reme_id INT,
  @orme_price MONEY,
  @orme_qty SMALLINT,
  @orme_discount SMALLMONEY,
  @orme_pay_type NCHAR(2),
  @orme_cardnumber NVARCHAR(25),
  @orme_user_id INT,
  @orme_status NVARCHAR(20)
AS
BEGIN
  SET NOCOUNT ON;

  -- Check if user_id exists and order status is Open
  IF EXISTS (SELECT 1 FROM Users.users WHERE user_id = @orme_user_id) AND @orme_status = 'Open'
  BEGIN
    DECLARE @orme_id INT

    -- Get orme_id for reference in omde_orme_id
    SELECT @orme_id = orme_id
    FROM Resto.order_menus
    WHERE orme_user_id = @orme_user_id AND orme_status = 'Open';

    -- Check if omde_reme_id exists in order_menu_detail
    IF EXISTS (SELECT 1 FROM Resto.order_menu_detail WHERE omde_orme_id = @orme_id AND omde_reme_id = @omde_reme_id)
    BEGIN
      -- Update orme_qty if omde_reme_id exists
      UPDATE Resto.order_menu_detail
      SET orme_qty = orme_qty + @orme_qty
      WHERE omde_orme_id = @orme_id AND omde_reme_id = @omde_reme_id;
    END
    ELSE
    BEGIN
      -- Insert new detail if omde_reme_id doesn't exist
      INSERT INTO Resto.order_menu_detail (orme_price, orme_qty, orme_discount, omde_orme_id, omde_reme_id)
      VALUES (@orme_price, @orme_qty, @orme_discount, @orme_id, @omde_reme_id);
    END
  END
  ELSE
  BEGIN
    DECLARE @orme_order_number NVARCHAR(55), @orme_id INT;

    -- Generate order number
    SELECT @orme_order_number = CONCAT('Menus#', CONVERT(NVARCHAR(10), GETDATE(), 112), '-',
                                        RIGHT('0000' + CAST((SELECT COUNT(*) FROM Resto.order_menus) + 1 AS NVARCHAR(4)), 4));

    -- Insert new order_menu record
    INSERT INTO Resto.order_menus (orme_order_number, orme_order_date, orme_pay_type, orme_cardnumber, orme_user_id, orme_status, orme_invoice)
    VALUES (@orme_order_number, GETDATE(), @orme_pay_type, @orme_cardnumber, @orme_user_id, @orme_status,
            CASE WHEN @orme_status = 'ordered' THEN CONCAT('INV-', @orme_order_number, '-001') ELSE NULL END);

    -- Get orme_id for reference in omde_orme_id
    SELECT @orme_id = SCOPE_IDENTITY();

    -- Insert new detail record
    INSERT INTO Resto.order_menu_detail (orme_price, orme_qty, orme_discount, omde_orme_id, omde_reme_id)
    VALUES (@orme_price, @orme_qty, @orme_discount, @orme_id, @omde_reme_id);
  END
END
