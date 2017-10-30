namespace BaseWebApi.Code.Constants
{
	public class LoggingEvents
	{
		public const  int InsertDBError = 1000;
		public const int TitleExistsInDB = 1010;
		public const int UpdateDBError = 1020;
		public const int DeleteDBError = 1030;
		public const int IdNotExists = 1040;
		public const int GetIncorrectId = 1050;
		public const int GetNoSuchIdInDB = 1060;
		public const int UpdateIdNotCongruented = 1070;
		public const int EmtptyModel = 1080;
		public const int NoSuchIdInDB = 1090;

		public const int ErrorDbConnection = 1100;
		public const int ErrorGettingId = 1110;	
	}
}