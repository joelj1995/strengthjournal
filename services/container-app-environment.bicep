param location string
param environmentName string
param containerDeploymentRevision string
param dotnetEnv string
param dotnetAzTenantId string
param dotnetAzClientId string
@secure()
param dotnetAzClientSecret string

resource law 'Microsoft.OperationalInsights/workspaces@2020-03-01-preview' = {
  name: '${environmentName}-logAnalytics'
  location: location
  properties: any({
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}

resource containerAppEnvironment 'Microsoft.App/managedEnvironments@2022-06-01-preview' = {
  name: '${environmentName}-containerAppEnvironment'
  location: location
  properties: {
    zoneRedundant: false
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: law.properties.customerId
        sharedKey: law.listKeys().primarySharedKey
      }
    }
  }
}

module containerAppMvc './StrengthJournal.MVC/container-app-mvc.bicep' = {
  name: '${environmentName}-containerApp-mvc'
  params: {
    location: location
    containerDeploymentRevision: containerDeploymentRevision
    environmentId: containerAppEnvironment.id
    dotnetEnv: dotnetEnv
    dotnetAzTenantId: dotnetAzTenantId
    dotnetAzClientId: dotnetAzClientId
    dotnetAzClientSecret: dotnetAzClientSecret
  }
}
