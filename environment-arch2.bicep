param location string = resourceGroup().location
param environmentName string = resourceGroup().name
param containerDeploymentRevision string

module containerAppEnvironment './services/container-app-environment.bicep' = {
  name: '${environmentName}-containerAppEnvironment'
  params: {
    environmentName: environmentName
    location: location
    containerDeploymentRevision: containerDeploymentRevision
  }
}
