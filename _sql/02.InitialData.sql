USE sitconsu_foresteye
GO

-- 1 Tipo Evento
-- 2 Motivo
-- 3 Tipo de Area
SET IDENTITY_INSERT TipoEvento ON
INSERT INTO TipoEvento (Id, Nombre) VALUES (	1	,	'Deforestaci�n');
INSERT INTO TipoEvento (Id, Nombre) VALUES (	2	,	'Incendio');
INSERT INTO TipoEvento (Id, Nombre) VALUES (	3	,	'Inundaci�n');
INSERT INTO TipoEvento (Id, Nombre) VALUES (	4	,	'Contaminaci�n');
SET IDENTITY_INSERT TipoEvento OFF

SET IDENTITY_INSERT Motivo ON
INSERT INTO Motivo (Id, Nombre) VALUES (	1	,	'Miner�a');
INSERT INTO Motivo (Id, Nombre) VALUES (	2	,	'Tala');
INSERT INTO Motivo (Id, Nombre) VALUES (	3	,	'Zona Cultivo');
SET IDENTITY_INSERT Motivo OFF

SET IDENTITY_INSERT TipoArea ON
INSERT INTO TipoArea (Id, Nombre) VALUES (	1	,	'Delimitaci�n Geogr�fica');
INSERT INTO TipoArea (Id, Nombre) VALUES (	2	,	'Area afectada del Evento');
INSERT INTO TipoArea (Id, Nombre) VALUES (	3	,	'Area protegida');
SET IDENTITY_INSERT TipoArea OFF
GO

SELECT * FROM TipoEvento
SELECT * FROM Motivo
SELECT * FROM TipoArea
