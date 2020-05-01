CREATE TABLE Cash_Payment
(
  payment_date DATETIME NOT NULL,
  payment_id INT identity(1,3) NOT NULL,
  payment_amount FLOAT NOT NULL,
  billing_id INT  NOT NULL,
  PRIMARY KEY (payment_id),
  FOREIGN KEY (billing_id) REFERENCES Billing(billing_id)
);