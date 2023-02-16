SELECT DATA_TYPE, column_name, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'resto_menus';

select * from resto.order_menus;

ALTER TABLE Resto.resto_menus
ALTER COLUMN reme_type nvarchar(20);

INSERT INTO Resto.order_menus (orme_order_date, orme_total_item, orme_total_discount, 
    orme_total_amount, orme_pay_type, orme_cardnumber, orme_is_paid, orme_modified_date, orme_user_id)
VALUES ('2023-02-16', 3, 5000, 25000, 'CR', '87654', 'P', '2023-02-16', 3)

ALTER TABLE Resto.order_menus
ALTER COLUMN orme_order_number nvarchar(55) NULL



SELECT DATA_TYPE, column_name, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'resto_menu_photos';

