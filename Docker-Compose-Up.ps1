param (
    [Parameter(Mandatory=$true)]
    [string]$Environment,

    [Parameter(Mandatory=$true)]
    [string]$RoleId,

    [Parameter(Mandatory=$true)]
    [string]$SecretId
)

$env:ASPNETCORE_ENVIRONMENT = $Environment
$env:HashiCorpVaultOptions__RoleId = $RoleId
$env:HashiCorpVaultOptions__SecretId = $SecretId

docker compose build
docker compose create hashicorp.vault.api
docker compose start hashicorp.vault.api
