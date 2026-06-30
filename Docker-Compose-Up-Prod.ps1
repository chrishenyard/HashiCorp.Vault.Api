param (
    [Parameter(Mandatory=$true)]
    [string]$Environment,

    [Parameter(Mandatory=$true)]
    [string]$RoleName
)

$env:ASPNETCORE_ENVIRONMENT = $Environment
$env:HashiCorpVaultOptions__RoleName = $RoleName

docker compose build
