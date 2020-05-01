CREATE TABLE [dbo].[Cash_Payment] (
    [payment_date]   DATETIME   NOT NULL,
    [payment_id]     INT        IDENTITY (1, 3) NOT NULL,
    [payment_amount] FLOAT (53) NOT NULL,
    [billing_id]     INT        NOT NULL,
	[receive_receipt] bit not null,
    PRIMARY KEY CLUSTERED ([payment_id] ASC),
    FOREIGN KEY ([billing_id]) REFERENCES [dbo].[Billing] ([billing_id])
);

