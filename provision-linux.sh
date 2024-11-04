sudo apt-get update
sudo apt-get upgrade -y

# Install dependencies
sudo apt-get install -y wget apt-transport-https software-properties-common

# Add Microsoft package link for repo
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Install .net sdk 6.0
sudo apt-get update
sudo apt-get install -y dotnet-sdk-6.0

# check dotnet version
dotnet --version

cd /home/vagrant/project

dotnet run --project /home/vagrant/project/Lab4/Lab4 run lab1 --input input.txt --output output.txt