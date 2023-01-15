param location string
param containerDeploymentRevision string
param environmentId string
param environmentDomain string
@secure()
param containerRegistryPassword string

resource containerAppServer 'Microsoft.App/containerApps@2022-03-01' = {
  name: 'server'
  location: location
  identity: { type: 'None' }
  properties: {
    configuration: {
      activeRevisionsMode: 'Single'
      dapr: null
      ingress: {
        allowInsecure: true
        customDomains: null
        external: true
        targetPort: 80
        transport: 'Auto'
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
          image: 'strengthjournalservices.azurecr.io/server:${containerDeploymentRevision}'
          name: 'server'
          env: [
            {
              name: 'FQDN'
              value: environmentDomain
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
