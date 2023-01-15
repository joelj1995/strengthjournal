
param location string
param containerDeploymentRevision string
param environmentId string
param dotnetEnv string
param dotnetAzTenantId string
param dotnetAzClientId string
@secure()
param dotnetAzClientSecret string
@secure()
param containerRegistryPassword string

resource containerAppIam 'Microsoft.App/containerApps@2022-03-01' = {
  name: 'iam'
  location: location
  identity: { type: 'None' }
  properties: {
    configuration: {
      activeRevisionsMode: 'Single'
      dapr: null
      ingress: {
        allowInsecure: true
        customDomains: null
        external: false
        targetPort: 80
        transport: 'http'
        traffic: [
          {
            weight: 100
            latestRevision: true
          }
        ]
      }
      registries: [
        {
          identity: ''
          server: 'strengthjournalservices.azurecr.io'
          passwordSecretRef: 'reg-pswd-a0579af6-bc30'
          username: 'StrengthJournalServices'
        }
      ]
      secrets: [
        {
          name: 'reg-pswd-a0579af6-bc30'
          value: containerRegistryPassword
        }
      ]
    }
    managedEnvironmentId: environmentId
    template: {
      containers: [
        {
          image: 'strengthjournalservices.azurecr.io/iam:${containerDeploymentRevision}'
          name: 'iam'
          env: [
            {
              name: 'ASPNETCORE_ENVIRONMENT'
              value: dotnetEnv
            }
            {
                name: 'AZURE_TENANT_ID'
                value: dotnetAzTenantId
            }
            {
                name: 'AZURE_CLIENT_ID'
                value: dotnetAzClientId
            }
            {
                name: 'AZURE_CLIENT_SECRET'
                value: dotnetAzClientSecret
            }
          ]
          probes: []
          resources: {
            cpu: json('0.25')
            memory: '0.5Gi'
          }
          volumeMounts: null
        }
      ]
      revisionSuffix: ''
      scale: {
        minReplicas: 0
        maxReplicas: 10
        rules: null
      }
      volumes: null
    }
  }
}
