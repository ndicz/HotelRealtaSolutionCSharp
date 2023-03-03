CREATE TRIGGER trg_update_order_menus_total_item
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


--------------------TRIGGER UNTUK THUMBNAIL---------------------------------------------


CREATE TRIGGER tr_set_remp_primary ON Resto.resto_menu_photos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
  SET NOCOUNT ON;

  -- jika terjadi update atau delete pada table Resto.resto_menu_photos
  IF EXISTS(SELECT * FROM deleted)
  BEGIN
    -- update remp_primary = 1 pada record dengan remp_id tertinggi yang memiliki remp_reme_id sama
    UPDATE Resto.resto_menu_photos
    SET remp_primary = 1
    FROM Resto.resto_menu_photos p1
    INNER JOIN (
      SELECT remp_reme_id, MAX(remp_id) AS max_remp_id
      FROM deleted
      GROUP BY remp_reme_id
    ) p2 ON p1.remp_reme_id = p2.remp_reme_id AND p1.remp_id = p2.max_remp_id;

    -- update remp_primary = 0 pada record yang tidak dipilih sebagai record utama
    UPDATE Resto.resto_menu_photos
    SET remp_primary = 0
    WHERE remp_reme_id IN (SELECT remp_reme_id FROM deleted)
      AND remp_id NOT IN (SELECT max_remp_id FROM (
        SELECT remp_reme_id, MAX(remp_id) AS max_remp_id
        FROM deleted
        GROUP BY remp_reme_id
      ) t)
  END
  ELSE -- jika terjadi insert pada table Resto.resto_menu_photos
  BEGIN
    -- update remp_primary = 1 pada record yang baru di-insert
    UPDATE Resto.resto_menu_photos
    SET remp_primary = 1
    WHERE remp_id IN (SELECT remp_id FROM inserted);

    -- update remp_primary = 0 pada record yang lain
    UPDATE Resto.resto_menu_photos
    SET remp_primary = 0
    WHERE remp_reme_id IN (SELECT remp_reme_id FROM inserted)
      AND remp_id NOT IN (SELECT remp_id FROM inserted);
  END
END;
