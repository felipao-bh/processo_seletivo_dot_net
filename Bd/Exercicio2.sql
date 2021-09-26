SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gonçalves, Felipe
-- Create date: 25/08/2021
-- Description:	Processo seletivo desenvolvedor .NET - CSU 
-- Stored procedure que retorna uma lista de Notas Fiscais do mês informado como parâmetro. 
-- =============================================
CREATE PROCEDURE SP_RetornaNF_Por_Mes	
	@MesBusca INTEGER	
AS
BEGIN

	SET NOCOUNT ON;

    SELECT * FROM NOTA_FISCAL NFS
	WHERE  MONTH(NFS.DTEMISSAO) = @MesBusca

END
GO
