c
CREATE PROCEDURE FindAllRestoMenus
AS
BEGIN
	select * from resto.resto_menus ORDER BY reme_id;
END