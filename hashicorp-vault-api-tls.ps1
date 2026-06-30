dotnet dev-certs https -ep ~/.aspnet/https/aspnetapp.pfx -p ""

kubectl delete secret hashicorp-vault-api-tls -n hashicorp-vault-api --ignore-not-found

kubectl create secret generic hashicorp-vault-api-tls `
  --from-file=aspnetapp.pfx="$HOME/.aspnet/https/aspnetapp.pfx" `
  -n hashicorp-vault-api

kubectl rollout restart deployment/hashicorp-vault-api -n hashicorp-vault-api
