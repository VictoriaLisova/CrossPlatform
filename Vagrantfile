# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure("2") do |config|

  config.vm.define "linux" do |linux|
    linux.vm.box = "ubuntu/focal64"
    linux.vm.box_version = "20240821.0.1"
    linux.vm.network "forwarded_port", guest: 80, host: 8080
    linux.vm.network "public_network"

    linux.vm.provider "virtualbox" do |vb|
      vb.name = "Linux"
      vb.memory = "1024"
    end
    linux.vm.synced_folder ".", "/home/vagrant/project"
    linux.vm.provision "shell", path: "provision-linux.sh"
  end

  config.vm.define "windows" do |windows|
    windows.vm.box = "gusztavvargadr/windows-10"
    # windows.vm.box_version = "2202.0.2409"
    windows.vm.network "forwarded_port", guest: 3389, host: 3389
    windows.vm.network "public_network"

    windows.vm.provider "virtualbox" do |vb|
      vb.name = "Windows"
      vb.memory = "4096"
    end
    windows.vm.synced_folder ".", "C:/project"
    windows.vm.provision "shell", path: "provision-windows.ps1"
  end

  config.vm.define "mac" do |mac|
    mac.vm.box = "ramsey/macos-catalina"
    mac.vm.box_version = "1.0.0"
    mac.vm.network "forwarded_port", guest: 8081, host: 8081
    # mac.vm.network "public_network"

    mac.vm.provider "virtualbox" do |vb|
      vb.name = "Mac"
      vb.memory = "2048"
    end
    mac.vm.synced_folder ".", "/Users/vagrant/project"
    mac.vm.provision "shell", path: "provision-mac.sh"
  end
end
