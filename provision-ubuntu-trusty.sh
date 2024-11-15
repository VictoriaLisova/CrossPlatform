sudo apt-get update
sudo apt-get upgrade -y

sudo apt-get install -y wget apt-transport-https software-properties-common

wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

dotnet --version

#go to the folder with api
cd /home/vagrant/webProject/Lab5_/API
# Run API
dotnet run --urls "https://localhost:5445"

# go tj the folder with client (web)
cd /home/vagrant/webProject/Lab5_/Lab5
# Run Lab
dotnet run --urls "https://localhost:5444"

# go to the folder with server 
cd /home/vagrant/webProject/Lab5_/Server
# Run Server
dotnet run --urls "https://localhost:5443"
