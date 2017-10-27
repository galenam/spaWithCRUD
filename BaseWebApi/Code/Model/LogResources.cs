using System.Collections.Generic;

namespace BaseWebApi.Code.Model
{
	public class LogResources
	{
		public LogResourcesEntity DefaultLogDescription { get; set; }
		public IEnumerable<LogResourcesEntity> ContollersLogDescription {get;set;}
	}
}