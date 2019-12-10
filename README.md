# Shop
Work with tim shop


PROCEDURE for get data 
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
-- =============================================  
-- Author:      Manoj Kalla  
-- Create date: 20-Nov-2017  
-- Description: Update a member detail by ID  
-- =============================================  
CREATE PROCEDURE spGetCategoryIds  
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
