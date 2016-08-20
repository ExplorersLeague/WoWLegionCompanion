using System;
using System.Collections.Generic;

namespace bgs.RPCServices
{
	public sealed class ServiceCollectionHelper
	{
		public void AddImportedService(uint serviceId, ServiceDescriptor serviceDescriptor)
		{
			this.importedServices.Add(serviceId, serviceDescriptor);
		}

		public void AddExportedService(uint serviceId, ServiceDescriptor serviceDescriptor)
		{
			this.exportedServices.Add(serviceId, serviceDescriptor);
		}

		public Dictionary<uint, ServiceDescriptor> ImportedServices
		{
			get
			{
				return this.importedServices;
			}
		}

		public ServiceDescriptor GetImportedServiceById(uint service_id)
		{
			if (this.importedServices == null)
			{
				return null;
			}
			ServiceDescriptor result;
			this.importedServices.TryGetValue(service_id, out result);
			return result;
		}

		public ServiceDescriptor GetImportedServiceByName(string name)
		{
			foreach (KeyValuePair<uint, ServiceDescriptor> keyValuePair in this.importedServices)
			{
				if (keyValuePair.Value.Name == name)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		public ServiceDescriptor GetExportedServiceById(uint service_id)
		{
			ServiceDescriptor result;
			this.exportedServices.TryGetValue(service_id, out result);
			return result;
		}

		private Dictionary<uint, ServiceDescriptor> importedServices = new Dictionary<uint, ServiceDescriptor>();

		private Dictionary<uint, ServiceDescriptor> exportedServices = new Dictionary<uint, ServiceDescriptor>();
	}
}
