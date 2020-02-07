Feature: Gold Loan Ticket Creation
	As a user, I want to verify the scenarios related to Gold Loan Ticket Creation

@Regression
Scenario: Verify the gold loan-NON approval ticket creation for ONE month period
	Given I Login to 'GOLDLOAN' application as 'Gold Loan Officer'
	Then I navigate to GOLD LOAN-Ticket Creation page
	And I include the customer NIC number to the NIC field on Customer Details panel in GOLD LOAN-Ticket Creation page
	When I click on the Search button on Customer Details panel in GOLD LOAN-Ticket Creation page
	Then The Customer Details are displayed on Validate Customer panel in GOLD LOAN-Ticket Creation page
	When I click on the Next button in GOLD LOAN-Ticket Creation page
	Then The Article Details panel is displayed in GOLD LOAN-Ticket Creation page
	And I include the details on Article Details panel in GOLD LOAN-Ticket Creation page as follows:	
	| FieldName         | FieldType              | FieldValues    |
	| Article Main Type | Dropdown               | Bangle         |
	| Sub Article Type  | Dropdown               | BANGLE         |
	| No of Items       | TextField              | 1              |
	| Quality Type      | MultiSelectionDropdown | Broken, Damage |
	| Karatage          | Dropdown               | 22             |
	| Gross Weight (g)  | TextField              | 1              |
	| Net Weight (g)    | TextField              | 1              |

	When I click on the Add button on Article Details panel in GOLD LOAN-Ticket Creation page
	Then The Article is added to the Article Details section on Article Details panel in GOLD LOAN-Ticket Creation page
	When I click on the Next button in GOLD LOAN-Ticket Creation page
	Then The Terms & Conditions panel is displayed in GOLD LOAN-Ticket Creation page
	And I include the details on Terms & Conditions panel in GOLD LOAN-Ticket Creation page as follows:
	| Field Name | FieldType | Field Value  |
	| Product    | Dropdown  | Normal       |
	| Period     | Dropdown  | 1 Month      |
	| Purpose    | Dropdown  | Construction |

	When I click on the Next button in GOLD LOAN-Ticket Creation page
	Then The Ticket Summery panel is displayed in GOLD LOAN-Ticket Creation page
	When I click on Done button in GOLD LOAN-Ticket Creation page
	Then The Ticket Created Successfully notification is displayed to the user
	And The Ticket was approved notification is displayed to the user
	When I Click on the Print button in GOLD LOAN-Ticket Creation page
	Then The Work flow has started notification is displayed to the user

	Given I Login to 'WORKBENCH' application as 'Gold Loan Officer'
	Then I navigate to WORKBENCH-Complete Activities page
	When I include the reference number to the search field on Approved tab panel in WORKBENCH-Complete Activities page
	Then I verify the approved gold loan ticket is listed in WORKBENCH-Complete Activities page
	And I logout from the eclipse application

	Given I Login to 'CASHIER' application as 'Cashier'
	Then I navigate to Withdrawal page
	And I include Reference Number to the Facility No field in Withdrawal page
	When I click on Search button in Withdrawal page
	Then The Ticket details are displayed in Withdrawal page
	And I verify Receipt Type field value is 'Granting' on Denominations panel in Withdrawal page 
	And I add denominations equal to the total of paying amount on Denominations panel in Withdrawal page
	When I click on Save button on Denominations panel in Withdrawal page 
	Then The Paying Successful notification is displayed to the user

	Given I Login to 'WORKBENCH' application as 'Cashier'
	Then I navigate to WORKBENCH-Complete Activities page
	When I include the reference number to the search field on Approved tab panel in WORKBENCH-Complete Activities page
	Then I verify the approved gold loan ticket is listed in WORKBENCH-Complete Activities page
	Then I verify the database records for '1 month' Gold Loan Granting transaction details