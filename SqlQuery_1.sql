SELECT DATA_TYPE, column_name, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'resto_menus';

select * from resto.resto_menus;

ALTER TABLE Resto.resto_menus
ALTER COLUMN reme_type nvarchar(20);

INSERT INTO Resto.order_menus (orme_order_date, orme_total_item, orme_total_discount, 
    orme_total_amount, orme_pay_type, orme_cardnumber, orme_is_paid, orme_modified_date, orme_user_id)
VALUES ('2023-02-16', 3, 5000, 25000, 'CA', '87654', 'P', '2023-02-16', 3)

ALTER TABLE Resto.order_menus
ALTER COLUMN orme_order_number nvarchar(55) NULL



SELECT DATA_TYPE, column_name, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'order_menus';



SELECT CAST(reme_price AS numeric(18,2)) as reme_price
FROM resto.resto_menus


Select reme_faci_id RemeFaciId, reme_id RemeId, reme_name RemeName, reme_description RemeDescription, reme_price RemePrice, reme_status RemeStatus, reme_modified_date RemeModifiedDate, reme_type Remetype from Resto.resto_menus


ALTER TABLE Resto.order_menus
ADD orme_status NVARCHAR(20) NOT NULL DEFAULT 'Open',
    invoice_number NVARCHAR(20) NULL;
   
   --Buat Invoice 

   select * from Resto.order_menu_detail
   
    Select * from resto.order_menus

    INSERT INTO resto.order_menu_detail
  (orme_price, orme_qty, orme_discount, omde_orme_id, omde_reme_id)
VALUES
  (0, 2, 5000, 1, 1);





