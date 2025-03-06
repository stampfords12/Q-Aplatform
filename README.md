CREATE TABLE QuestionAnswer (
	QuestionId INT,
	Ref_QuestionId INT,
	Type INT NOT NULL,
	CustomerId INT,
	Tx_Value NVARCHAR(MAX),
	Create_Dt DATETIME,
	Update_Dt DATETIME,
	Point INT,
	Tag NVARCHAR(MAX)
);
