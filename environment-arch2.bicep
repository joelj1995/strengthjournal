param location string = resourceGroup().location
param environmentName string = resourceGroup().name

module containerAppEnvironment './services/container-app-environment.bicep' = {
  name: '${environmentName}-containerAppEnvironment'
  params: {
    environmentName: environmentName
    location: location
  }
}
