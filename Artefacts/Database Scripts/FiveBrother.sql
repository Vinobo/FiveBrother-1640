use master
USE FiveBrother
GO
CREATE TABLE LikeIdea	
(
	Idea_ID INT FOREIGN KEY REFERENCES Idea(Idea_ID),
	User_ID INT FOREIGN KEY REFERENCES UserAccount(User_ID),
	LikeIdea bit
)