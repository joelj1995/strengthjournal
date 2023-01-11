
param location string
param containerDeploymentRevision string
param environmentId string

resource containerAppMvc 'Microsoft.App/containerApps@2022-03-01' = {
  name: 'mvc'
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
          value: 'pG/IlLZjtIv1OODVlPYXRyTXOf/kE+A/oItn2MmnUU+ACRCPSeqy'
        }
      ]
    }
    managedEnvironmentId: environmentId
    template: {
      containers: [
        {
          image: 'strengthjournalservices.azurecr.io/mvc:${containerDeploymentRevision}'
          name: 'mvc'
          env: [
            {
              name: 'ASPNETCORE_ENVIRONMENT'
              value: 'test'
            }
            {
                name: 'AZURE_TENANT_ID'
                value: '31e50e54-3b13-47ed-938e-8600cb767ae7'
            }
            {
                name: 'AZURE_CLIENT_ID'
                value: 'f15b8bb4-c5b6-410f-b185-ca5c4c5619cc'
            }
            {
                name: 'AZURE_CLIENT_SECRET'
                value: '4kk8Q~SBAYVMXcEKWrGn1iVyYb0TptYd43SF7b1r'
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
