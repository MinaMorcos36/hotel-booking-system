INSERT INTO dbo.RoomCategories (Name, Capacity, Price)
VALUES
    (N'Single', 1,  50.00),
    (N'Double', 2,  80.00),
    (N'Triple', 3, 110.00),
    (N'Suite',  4, 200.00);
GO

INSERT INTO dbo.Rooms (Number, [Floor], CategoryId)
SELECT v.Number, v.[Floor], c.Id
FROM (VALUES
        (N'101', 1, N'Single'),
        (N'102', 1, N'Double'),
        (N'103', 1, N'Double'),
        (N'201', 2, N'Triple'),
        (N'202', 2, N'Suite')
     ) AS v (Number, [Floor], CategoryName)
JOIN dbo.RoomCategories AS c ON c.Name = v.CategoryName;
GO

INSERT INTO dbo.Guests (Name, Phone, Email)
VALUES (N'Test Guest', N'+20 100 000 0000', N'test.guest@example.com');
GO
