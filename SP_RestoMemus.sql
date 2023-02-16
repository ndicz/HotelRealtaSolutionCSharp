CREATE SEQUENCE seq_order_number
START WITH 1
INCREMENT BY 1;
GO

CREATE TRIGGER tr_order_menus_insert
ON Resto.order_menus
INSTEAD OF INSERT
AS
BEGIN
  DECLARE @prefix nvarchar(20) = 'MENUS#'
  DECLARE @date nvarchar(20) = CONVERT(nvarchar(8), GETDATE(), 112)
  DECLARE @next_order_number int

  -- Get the next order number from the sequence
  SELECT @next_order_number = NEXT VALUE FOR seq_order_number

  -- Insert the rows with the updated order numbers
  INSERT INTO Resto.order_menus (orme_order_number, orme_order_date, orme_total_item, orme_total_discount, orme_total_amount, orme_pay_type, orme_cardnumber, orme_is_paid, orme_modified_date, orme_user_id)
  SELECT 
    @prefix + @date + '-' + RIGHT(REPLICATE('0', 4) + CAST(@next_order_number AS nvarchar(4)), 4), 
    orme_order_date, 
    orme_total_item, 
    orme_total_discount, 
    orme_total_amount, 
    orme_pay_type, 
    orme_cardnumber, 
    orme_is_paid, 
    orme_modified_date, 
    orme_user_id
  FROM inserted
END




CREATE TRIGGER tr_set_cardnumber_to_dash
ON Resto.order_menus
AFTER INSERT, UPDATE
AS
BEGIN
    IF UPDATE(orme_pay_type) -- check if orme_pay_type column was updated
    BEGIN
        UPDATE Resto.order_menus
        SET orme_cardnumber = '-'
        WHERE orme_id IN (SELECT inserted.orme_id FROM inserted WHERE inserted.orme_pay_type = 'CA')
    END
END

CREATE TRIGGER tr_order_menus_insert
ON Resto.order_menus
INSTEAD OF INSERT
AS
BEGIN
  DECLARE @prefix nvarchar(20) = 'MENUS#'
  DECLARE @date nvarchar(20) = CONVERT(nvarchar(8), GETDATE(), 112)
  DECLARE @next_order_number int

  -- Get the last order number and date
  DECLARE @last_order_number int
  DECLARE @last_order_date nvarchar(8)
  SELECT TOP 1 @last_order_number = RIGHT(orme_order_number, 4), @last_order_date = SUBSTRING(orme_order_number, 7, 8) 
  FROM Resto.order_menus
  ORDER BY orme_order_number DESC

  -- Check if the last order date is the same as today's date
  IF @last_order_date = @date
  BEGIN
    -- If yes, get the next order number from the sequence
    SELECT @next_order_number = NEXT VALUE FOR seq_order_number
  END
  ELSE
  BEGIN
    -- If no, reset the sequence and start from 1
    ALTER SEQUENCE seq_order_number RESTART WITH 1
    SELECT @next_order_number = NEXT VALUE FOR seq_order_number
  END

  -- Insert the rows with the updated order numbers
  INSERT INTO Resto.order_menus (orme_order_number, orme_order_date, orme_total_item, orme_total_discount, orme_total_amount, orme_pay_type, orme_cardnumber, orme_is_paid, orme_modified_date, orme_user_id)
  SELECT 
    @prefix + @date + '-' + RIGHT(REPLICATE('0', 4) + CAST(@next_order_number AS nvarchar(4)), 4), 
    orme_order_date, 
    orme_total_item, 
    orme_total_discount, 
    orme_total_amount, 
    orme_pay_type, 
    orme_cardnumber, 
    orme_is_paid, 
    orme_modified_date, 
    orme_user_id
  FROM inserted
END
