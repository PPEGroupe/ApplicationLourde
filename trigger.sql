CREATE TRIGGER TriggerInsteadOfDeleteOffer 
ON Offer INSTEAD OF DELETE
AS
	UPDATE Offer
	SET IsDeleted = 1
	WHERE Identifier IN (
		SELECT Identifier FROM deleted
	);
GO