CREATE TABLE dbo.RoomCategories
(
    Id        int            IDENTITY(1,1) NOT NULL,
    Name      nvarchar(50)   NOT NULL,
    Capacity  int            NOT NULL,
    Price     decimal(10,2)  NOT NULL,

    CONSTRAINT PK_RoomCategories          PRIMARY KEY (Id),
    CONSTRAINT UQ_RoomCategories_Name     UNIQUE (Name),
    CONSTRAINT CK_RoomCategories_Capacity CHECK (Capacity > 0),
    CONSTRAINT CK_RoomCategories_Price    CHECK (Price >= 0)
);
GO

CREATE TABLE dbo.BookingStatuses
(
    Id    int           NOT NULL,
    Name  nvarchar(30)  NOT NULL,

    CONSTRAINT PK_BookingStatuses      PRIMARY KEY (Id),
    CONSTRAINT UQ_BookingStatuses_Name UNIQUE (Name)
);
GO


CREATE TABLE dbo.Rooms
(
    Id           int           IDENTITY(1,1) NOT NULL,
    Number       nvarchar(10)  NOT NULL,
    [Floor]      int  NOT NULL,
    CategoryId   int           NOT NULL,
    IsAvailable  bit           NOT NULL
        CONSTRAINT DF_Rooms_IsAvailable DEFAULT (1),

    CONSTRAINT PK_Rooms          PRIMARY KEY (Id),
    CONSTRAINT UQ_Rooms_Number   UNIQUE (Number),
    CONSTRAINT FK_Rooms_RoomCategories
        FOREIGN KEY (CategoryId) REFERENCES dbo.RoomCategories (Id)
);
GO

CREATE TABLE dbo.Guests
(
    Id     int            IDENTITY(1,1) NOT NULL,
    Name   nvarchar(100)  NOT NULL,
    Phone  nvarchar(20)   NOT NULL,
    Email  nvarchar(254)  NOT NULL,

    CONSTRAINT PK_Guests        PRIMARY KEY (Id),
    CONSTRAINT UQ_Guests_Phone  UNIQUE (Phone),
    CONSTRAINT UQ_Guests_Email  UNIQUE (Email)
);
GO

CREATE TABLE dbo.Bookings
(
    Id         int         IDENTITY(1,1) NOT NULL,
    GuestId    int         NOT NULL,
    StatusId   int         NOT NULL,
    CheckIn    date        NOT NULL,
    CheckOut   date        NOT NULL,
    CreatedAt  datetime2(0) NOT NULL
        CONSTRAINT DF_Bookings_CreatedAt DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT PK_Bookings PRIMARY KEY (Id),
    CONSTRAINT CK_Bookings_Dates CHECK (CheckOut > CheckIn),
    CONSTRAINT FK_Bookings_Guests
        FOREIGN KEY (GuestId)  REFERENCES dbo.Guests (Id),
    CONSTRAINT FK_Bookings_BookingStatuses
        FOREIGN KEY (StatusId) REFERENCES dbo.BookingStatuses (Id)
);
GO

CREATE TABLE dbo.BookingRooms
(
    BookingId      int            NOT NULL,
    RoomId         int            NOT NULL,
    PricePerNight  decimal(10,2)  NOT NULL,
    GuestCount     int            NOT NULL,

    CONSTRAINT PK_BookingRooms PRIMARY KEY (BookingId, RoomId),
    CONSTRAINT CK_BookingRooms_PricePerNight CHECK (PricePerNight >= 0),
    CONSTRAINT CK_BookingRooms_GuestCount    CHECK (GuestCount > 0),
    CONSTRAINT FK_BookingRooms_Bookings
        FOREIGN KEY (BookingId) REFERENCES dbo.Bookings (Id),
    CONSTRAINT FK_BookingRooms_Rooms
        FOREIGN KEY (RoomId)    REFERENCES dbo.Rooms (Id)
);
GO

CREATE INDEX IX_Rooms_CategoryId     ON dbo.Rooms (CategoryId);
CREATE INDEX IX_Bookings_GuestId     ON dbo.Bookings (GuestId);
CREATE INDEX IX_Bookings_StatusId    ON dbo.Bookings (StatusId);
CREATE INDEX IX_Bookings_Dates       ON dbo.Bookings (CheckIn, CheckOut);
CREATE INDEX IX_BookingRooms_RoomId  ON dbo.BookingRooms (RoomId);
GO
