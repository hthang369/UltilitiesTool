DECLARE @store_name SYSNAME
DECLARE @store SYSNAME
DECLARE 
      @object_name SYSNAME
    , @object_id INT
DECLARE @type_combo varchar(10)

SET @store_name = '''ICMaterialGroupItems'''
set @type_combo = 'p'

IF @store_name IS NULL OR @store_name = ''
	DECLARE cs_store CURSOR SCROLL FOR SELECT name FROM sys.objects WHERE [type] IN(@type_combo)
ELSE
	exec('DECLARE cs_store CURSOR SCROLL FOR SELECT name FROM sys.objects WHERE name IN('+@store_name+')')

OPEN cs_store
WHILE 0 = 0
BEGIN
	FETCH NEXT FROM cs_store INTO @store
	IF @@FETCH_STATUS <> 0
		BREAK
	
	select [text] from sys.objects join sys.syscomments on [object_id] = [id] where name like '%'+@store+'%'
END
CLOSE cs_store
DEALLOCATE cs_store
