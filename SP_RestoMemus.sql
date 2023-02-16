CREATE SEQUENCE seq_order_number
START WITH 1
INCREMENT BY 1;

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
