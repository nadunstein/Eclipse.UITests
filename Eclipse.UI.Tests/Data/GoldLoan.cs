using Dapper;

namespace Eclipse.UI.Tests.Data
{
    public class GoldLoan
    {
        public static string GetGoldLoanIsBranchCustomerNicNumber()
        {
            const string mainSql = @"SELECT TOP 1 NICNO
                                       FROM GLN.TICKET T
                                 INNER JOIN MSD.BUSINESSPARTNER B
                                         ON T.BUSINESSPARTNERID =B.ID
                                      WHERE BRANCHID = 2
                                        AND NICNO IS NOT NULL";

            var nicNumber = ConnectionManager.ExecuteReturn(connection =>
                connection.QueryFirstOrDefault<string>(mainSql));
            return nicNumber;
        }

        public static int GetNumberOfRecordsInAccount(string referenceNumber)
        {
            const string mainSql = @"SELECT COUNT(A.ACCAMOUNT)
                                       FROM DESTINITY_LEASE.DBO.ACCTXNDETAILS A
                                 INNER JOIN DESTINITY_LEASE.DBO.ACCSUBACCOUNTREF AR 
                                         ON A.ACCCODE = AR.ACCCODE 
                                      WHERE NARRATION LIKE @formattedRefNo";

            string formattedRefNo = $"%{referenceNumber}%";
            var count = ConnectionManager.ExecuteReturn(connection =>
                connection.QueryFirstOrDefault<int>(mainSql,
                    new { formattedRefNo }));
            return count;
        }

        public static double GetTransactionAmount(string transaction, string referenceNumber)
        {
            const string mainSql = @"SELECT A.ACCAMOUNT
                                       FROM DESTINITY_LEASE.DBO.ACCTXNDETAILS A
                                 INNER JOIN DESTINITY_LEASE.DBO.ACCSUBACCOUNTREF AR 
                                         ON A.ACCCODE = AR.ACCCODE 
                                      WHERE NARRATION LIKE @formattedRefNo
                                        AND AR.SUBACCOUNTNAME = @transaction";

            string formattedRefNo = $"%{referenceNumber}%";
            var amount = ConnectionManager.ExecuteReturn(connection =>
                connection.QueryFirstOrDefault<string>(mainSql,
                    new { transaction, formattedRefNo }));
            return double.Parse(amount);
        }
    }
}