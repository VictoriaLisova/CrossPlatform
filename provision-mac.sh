/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

echo 'eval "$(/opt/homebrew/bin/brew shellenv)"' >> /Users/vagrant/.zprofile
eval "$(/opt/homebrew/bin/brew shellenv)"

brew install --cask dotnet-sdk

dotnet --version

cd /Users/vagrant/project

dotnet run --project /Users/vagrant/project/Lab4/Lab4 run lab1 --input input.txt --output output.txt