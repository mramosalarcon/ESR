USE [ESR_2022]
GO
/****** Object:  StoredProcedure [dbo].[CESR_EliminaEvidenciaIndicador]    Script Date: 8/26/2021 2:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CESR_EliminaEvidenciaIndicador] 
	(
	@idIndicador int,
	@idTema int,
	@idTipoEvidencia int,
	@idEmpresa int,
	@idCuestionario int,
	@descripcion varchar(100)
	)
	
AS
declare @idCuestionarioAnterior int
Declare @idCuestionarioPreAnterior int
Declare @idCuestionarioPrePreAnterior int

	select @idCuestionarioAnterior = anterior from Cuestionario where idCuestionario = @idCuestionario
	select @idCuestionarioPreAnterior = anterior from Cuestionario where idCuestionario = @idCuestionarioAnterior
	select @idCuestionarioPrePreAnterior = anterior from Cuestionario where idCuestionario = @idCuestionarioPreAnterior

	if exists (Select
					evidencia
				From
					Evidencia_Indicador
				Where
					idIndicador = @idIndicador And
					idTema = @idTema And
					idTipoEvidencia = @idTipoEvidencia And
					idEmpresa = @idEmpresa And
					idCuestionario = @idCuestionario And
					descripcion = @descripcion and copia = 1)
		Update
			Evidencia_Indicador
		set 
				copia = 0,
				fechaModificacion = getdate(),
				idCuestionarioNoCopia = @idCuestionario
		Where
			idIndicador = @idIndicador And
			idTema = @idTema And
			idTipoEvidencia = @idTipoEvidencia And
			idEmpresa = @idEmpresa And
			idCuestionario = @idCuestionario And
			descripcion = @descripcion and
			copia = 1
			
	if exists (Select
					evidencia
				From
					ESR_2021.dbo.Evidencia_Indicador
				Where
					idIndicador = @idIndicador And
					idTema = @idTema And
					idTipoEvidencia = @idTipoEvidencia And
					idEmpresa = @idEmpresa And
					idCuestionario = @idCuestionarioAnterior And
					descripcion = @descripcion and copia = 1)
		Update
			ESR_2021.dbo.Evidencia_Indicador
		set 
			copia = 0,
			fechaModificacion = getdate(),
			idCuestionarioNoCopia = @idCuestionario
		Where
			idIndicador = @idIndicador And
			idTema = @idTema And
			idTipoEvidencia = @idTipoEvidencia And
			idEmpresa = @idEmpresa And
			idCuestionario = @idCuestionarioAnterior And
			descripcion = @descripcion	and
			copia = 1
	if exists (Select
				evidencia
			From
				ESR_2020.dbo.Evidencia_Indicador
			Where
				idIndicador = @idIndicador And
				idTema = @idTema And
				idTipoEvidencia = @idTipoEvidencia And
				idEmpresa = @idEmpresa And
				idCuestionario = @idCuestionarioPreAnterior And
				descripcion = @descripcion and copia = 1)
		Update
			ESR_2020.dbo.Evidencia_Indicador
		set
			copia = 0,
			fechaModificacion = getdate(),
			idCuestionarioNoCopia = @idCuestionario
		Where
			idIndicador = @idIndicador And
			idTema = @idTema And
			idTipoEvidencia = @idTipoEvidencia And
			idEmpresa = @idEmpresa And
			idCuestionario = @idCuestionarioPreAnterior And
			descripcion = @descripcion and
			copia = 1
			
	if exists (Select
					evidencia
				From
					ESR_2019.dbo.Evidencia_Indicador
				Where
					idIndicador = @idIndicador And
					idTema = @idTema And
					idTipoEvidencia = @idTipoEvidencia And
					idEmpresa = @idEmpresa And
					idCuestionario = @idCuestionarioPrePreAnterior And
					descripcion = @descripcion and copia = 1)
		Update
			ESR_2019.dbo.Evidencia_Indicador
		set
			copia = 0,
			fechaModificacion = getdate(),
			idCuestionarioNoCopia = @idCuestionario
		Where
			idIndicador = @idIndicador And
			idTema = @idTema And
			idTipoEvidencia = @idTipoEvidencia And
			idEmpresa = @idEmpresa And
			idCuestionario = @idCuestionarioPrePreAnterior And
			descripcion = @descripcion and
			copia = 1

	/* SET NOCOUNT ON */ 
	RETURN
