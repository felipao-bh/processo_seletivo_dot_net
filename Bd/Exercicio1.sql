-- =============================================
-- Author:		Gonçalves, Felipe
-- Create date: 25/08/2021
-- Description:	Processo seletivo desenvolvedor .NET - CSU 
-- Query que retorna o valor total e o total de produtos vendidos no mês de dezembro.

    SELECT	SUM(NFI.VALORTOTAL) AS SOMAVALOR,
			SUM(NFI.QTDE) AS SOMAQTDE
      FROM	NOTA_FISCAL NFS
INNER JOIN  NOTAFISCALITENS NFI ON NFI.CODNOTA = NFS.CODNOTA
     WHERE  MONTH(NFS.DTEMISSAO) = 12