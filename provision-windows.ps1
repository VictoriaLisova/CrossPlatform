Set-ExecutionPolicy Bypass -Scope Process -Force
[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

choco install dotnet-6.0-sdk -y

dotnet --version

cd "C:\project"

dotnet run --project "C:\project\Lab4\Lab4" run lab1 --input input.txt --output output.txt