CREATE TABLE [dbo].[Check_Payment] (
    [check_no]        INT        NOT NULL,
    [payment_amount]  FLOAT (53) NOT NULL,
    [payment_date]    DATE       NOT NULL,
    [billing_id]      INT        NOT NULL,
    [receive_receipt] BIT        NOT NULL,
    PRIMARY KEY CLUSTERED ([check_no] ASC),
    FOREIGN KEY ([billing_id]) REFERENCES [dbo].[Billing] ([billing_id])
);

