namespace BaseWebApi.Code.Constants
{
	public class LoggingEvents
	{
		public const  int InsertDBError = 1000;
		public const int TitleExistsInDB = 1010;
		public const int UpdateDBError = 1020;
		public const int DeleteDBError = 1030;
		public const int DepartmentWithIdNotExists = 1040;
//		public const int DepartmentWebApiGetIncorrectId = 1050;
//		public const int DepartmentWebApiGetNoSuchIdInDB = 1060;
		public const int DepartmentWebApiUpdateIdNotCongruented = 1070;
		public const int EmtptyDepartment = 1080;
		public const int NoSuchIdInDBDepartment = 1090;

		public const int ErrorDbConnection = 1100;
		public const int ErrorGettingId = 1110;	
		public const int GetIncorrectId = 1050;
	}
}