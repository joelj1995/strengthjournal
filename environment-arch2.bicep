param location string = resourceGroup().location
param environmentName string = resourceGroup().name
param containerDeploymentRevision string
param dotnetEnv string
param dotnetAzTenantId string
param dotnetAzClientId string
@secure()
param dotnetAzClientSecret string

module containerAppEnvironment './services/container-app-environment.bicep' = {
  name: '${environmentName}-containerAppEnvironment'
  params: {
    environmentName: environmentName
    location: location
    containerDeploymentRevision: containerDeploymentRevision
    dotnetEnv: dotnetEnv
    dotnetAzTenantId: dotnetAzTenantId
    dotnetAzClientId: dotnetAzClientId
    dotnetAzClientSecret: dotnetAzClientSecret
  }
}
