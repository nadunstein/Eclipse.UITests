using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Eclipse.UI.Tests.Features.GoldLoan
{
    [Binding]
    public class CommonSteps : BaseStep
    {
        private readonly ScenarioContext _scenarioContext;

        public CommonSteps(IWebDriver driver, ScenarioContext scenarioContext) : base(driver)
        {
            _scenarioContext = scenarioContext;
            InitializePageObjects();
        }

        [Then(@"I verify the database records for '(.*)' Gold Loan Granting transaction details")]
        public void ThenIVerifyTheDatabaseRecordsForGoldLoanGrantingTransactionDetails(string period)
        {
            VerifyNumberOfRecordsInAccount();
            VerifyGoldLoanCapitalControlAcAmount();
            VerifyCashInHandAmount();
            VerifyCreditAccountOutstandingAmount();
            BranchToHeadOfficeHeadOfficeToBranchAmount();
        }

        private void VerifyNumberOfRecordsInAccount()
        {
            var referenceNumber = _scenarioContext["ReferenceNumber"].ToString();
            Assert.AreEqual(6, Data.GoldLoan.GetNumberOfRecordsInAccount(referenceNumber),
                    "The number of records in Account are different for ONE month Gold Loan Granting transaction");
        }

        private void VerifyGoldLoanCapitalControlAcAmount()
        {
            const string transaction = "GOLD LOAN CAPITAL CONTROL AC";
            var referenceNumber = _scenarioContext["ReferenceNumber"].ToString();
            var transactionAmount = string.Format("{0:0.00}", 
                Data.GoldLoan.GetTransactionAmount(transaction, referenceNumber));
            var outstandingAmount = _scenarioContext["AdvanceOutstandingAmount"].ToString();
            Assert.AreEqual(outstandingAmount, transactionAmount, 
                "The 'GOLD LOAN CAPITAL CONTROL AC' amount is different");
        }

        private void VerifyCashInHandAmount()
        {
            const string transaction = "CASH IN HAND";
            var referenceNumber = _scenarioContext["ReferenceNumber"].ToString();
            var transactionAmount = string.Format("{0:0.00}",
                Data.GoldLoan.GetTransactionAmount(transaction, referenceNumber));
            var outstandingAmount = _scenarioContext["CashInHandAmount"].ToString();
            Assert.AreEqual(outstandingAmount, transactionAmount, "The 'CASH IN HAND' amount is different");
        }

        private void VerifyCreditAccountOutstandingAmount()
        {
            const string transactionOne = "CASH IN HAND";
            const string transactionTwo = "GOLD LOAN SERVICE CHARGERS AC";
            const string transactionThree = "VALUE ADDED TAX";
            var referenceNumber = _scenarioContext["ReferenceNumber"].ToString();
            var outstandingAmount = _scenarioContext["AdvanceOutstandingAmount"].ToString();
            var TotalAmountOfCreditTransactions = Data.GoldLoan.GetTransactionAmount(transactionOne, referenceNumber) 
                + Data.GoldLoan.GetTransactionAmount(transactionTwo, referenceNumber) 
                + Data.GoldLoan.GetTransactionAmount(transactionThree, referenceNumber);
            var formattedStringAmount = string.Format("{0:0.00}", TotalAmountOfCreditTransactions);
            Assert.AreEqual(outstandingAmount, formattedStringAmount, 
                "The credit account outstanding amount is different");
        }

        private void BranchToHeadOfficeHeadOfficeToBranchAmount()
        {
            const string branchToHeadOfficeTransaction = "CORPORATE BRANCH TO HEAD OFFICE";
            const string headOfficeToBranchTransaction = "HEAD OFFICE TO CORPORATE BRANCH";
            var referenceNumber = _scenarioContext["ReferenceNumber"].ToString();
            var branchToHeadOfficeTransactionAmount = 
                Data.GoldLoan.GetTransactionAmount(branchToHeadOfficeTransaction, referenceNumber);
            var headOfficeToBranchTransactionAmount =
                Data.GoldLoan.GetTransactionAmount(headOfficeToBranchTransaction, referenceNumber);
            Assert.AreEqual(branchToHeadOfficeTransactionAmount, headOfficeToBranchTransactionAmount, 
                "THe branch to head office and head office to branch amounts are different");
        }

        private void InitializePageObjects()
        {

        }
    }
}