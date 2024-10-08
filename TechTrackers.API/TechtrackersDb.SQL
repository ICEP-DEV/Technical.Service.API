-- Create DEPARTMENT table
CREATE TABLE Department (
    Department_ID INT PRIMARY KEY,
    Department_name NVARCHAR(100) NOT NULL
);

-- Create ROLE table
CREATE TABLE Role (
    Role_ID INT IDENTITY(1,1) PRIMARY KEY,
    Role_name NVARCHAR(50),
    Description NVARCHAR(MAX)
);

-- Create USER table
CREATE TABLE Users (
    User_ID INT IDENTITY(1,1) PRIMARY KEY,
    Surname NVARCHAR(100),
    Initials NVARCHAR(10),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Pass_word NVARCHAR(255) NOT NULL,
    Department_ID INT,
    Role_ID INT,
    FOREIGN KEY (Department_ID) REFERENCES Department(Department_ID),
    FOREIGN KEY (Role_ID) REFERENCES Role(Role_ID)
);

-- Create USER_ROLE table (for possible role management)
CREATE TABLE User_Role (
    User_Role_ID INT IDENTITY(1,1) PRIMARY KEY,
    User_ID INT,
    Role_ID INT,
    FOREIGN KEY (User_ID) REFERENCES Users(User_ID),
    FOREIGN KEY (Role_ID) REFERENCES Role(Role_ID)
);

-- Create CATEGORY table
CREATE TABLE Category (
    Category_ID INT IDENTITY(1,1) PRIMARY KEY,
    Category_name NVARCHAR(100) NOT NULL
);

-- Create LOG table
CREATE TABLE Log (
    Log_ID INT IDENTITY(1,1) PRIMARY KEY,
    Description NVARCHAR(MAX),
    Priority NVARCHAR(10) CHECK (Priority IN ('LOW', 'MEDIUM', 'HIGH')),
    Due_date DATETIME,
    Attachment_url NVARCHAR(MAX),
    Assigned_by INT,
    Assigned_at NVARCHAR(20) NOT NULL,
    Log_Status NVARCHAR(20) CHECK (Log_Status IN ('PENDING', 'IN PROGRESS', 'RESOLVED', 'ON HOLD')),
    Technician_ID INT,
    Staff_ID INT,
    SLA_ID INT,
    Category_ID INT,
    Created_at DATETIME DEFAULT GETDATE(),
    Updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (Assigned_by) REFERENCES Users(User_ID),
    FOREIGN KEY (Technician_ID) REFERENCES Users(User_ID),
    FOREIGN KEY (Staff_ID) REFERENCES Users(User_ID),
    FOREIGN KEY (SLA_ID) REFERENCES SLA(SLA_ID),
    FOREIGN KEY (Category_ID) REFERENCES Category(Category_ID)
);

-- Create SERVICE LEVEL AGREEMENT (SLA) table
CREATE TABLE SLA (
    SLA_ID INT IDENTITY(1,1) PRIMARY KEY,
    Log_ID INT,
    Description NVARCHAR(MAX),
    Resolution_Timeframe INT, -- Time in hours
    Priority_Level NVARCHAR(10) CHECK (Priority_Level IN ('LOW', 'MEDIUM', 'HIGH')),
    FOREIGN KEY (Log_ID) REFERENCES Log(Log_ID)
);

-- Create LOG_CHAT table
CREATE TABLE Log_Chat (
    Log_Chat_ID INT IDENTITY(1,1) PRIMARY KEY,
    Log_ID INT,
    Sender_ID INT,
    Message NVARCHAR(MAX) NOT NULL,
    Timestamp DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (Log_ID) REFERENCES Log(Log_ID),
    FOREIGN KEY (Sender_ID) REFERENCES Users(User_ID)
);

-- Create LOG_STATUS_HISTORY table
CREATE TABLE Log_Status_History (
    Log_Status_History_ID INT IDENTITY(1,1) PRIMARY KEY,
    Log_ID INT,
    Changed_by_User_ID INT,
    Log_Status NVARCHAR(20) CHECK (Log_Status IN ('PENDING', 'IN PROGRESS', 'RESOLVED', 'ON HOLD')),
    Updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (Log_ID) REFERENCES Log(Log_ID),
    FOREIGN KEY (Changed_by_User_ID) REFERENCES Users(User_ID)
);

-- Create FEEDBACK table
CREATE TABLE Feedback (
    Feedback_ID INT IDENTITY(1,1) PRIMARY KEY,
    Log_ID INT,
    User_ID INT,
    Rating NVARCHAR(10) CHECK (Rating IN ('EXCELLENT', 'GOOD', 'AVERAGE', 'POOR', 'TERRIBLE')) NOT NULL,
    Comments NVARCHAR(MAX),
    Feedback_Timestamp DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (Log_ID) REFERENCES Log(Log_ID),
    FOREIGN KEY (User_ID) REFERENCES Users(User_ID)
);

-- Create ESCALATION table
CREATE TABLE Escalation (
    Escalation_ID INT IDENTITY(1,1) PRIMARY KEY,
    Log_ID INT,
    HOD_ID INT,
    Reason NVARCHAR(MAX) NOT NULL,
    Time_stamp DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (Log_ID) REFERENCES Log(Log_ID),
    FOREIGN KEY (HOD_ID) REFERENCES Users(User_ID)
);

-- Create NOTIFICATION table
CREATE TABLE Notification (
    Notification_ID INT IDENTITY(1,1) PRIMARY KEY,
    Log_ID INT,
    User_ID INT,
    Message NVARCHAR(MAX),
    Type NVARCHAR(20) CHECK (Type IN ('INFORMATION', 'WARNING', 'ALERT')),
    Time_stamp DATETIME DEFAULT GETDATE(),
    Read_Status BIT DEFAULT 0,
    FOREIGN KEY (Log_ID) REFERENCES Log(Log_ID),
    FOREIGN KEY (User_ID) REFERENCES Users(User_ID)
);
