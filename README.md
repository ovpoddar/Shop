# Shop
Work with tim shop

GO

/****** Object:  StoredProcedure [dbo].[spGetCategoryIds]    Script Date: 02/12/2019 20:33:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spGetCategoryIds]

@id int

AS
BEGIN

	SET NOCOUNT ON;

    ;WITH cte AS(
        SELECT  *
        FROM    Categories
        WHERE   Id = @id
        UNION ALL
        SELECT  c.*
        FROM    Categories c INNER JOIN
                cte  ON c.ParentID = cte.ID
)

SELECT  *
FROM  cte

END
GO
