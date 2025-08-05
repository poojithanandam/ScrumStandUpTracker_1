pipeline {
    agent any

    tools {
        dotnet 'dotnet6'  // Match the name of your configured .NET SDK in Jenkins Global Tool Configuration
    }

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = "1"
    }

    stages {
        stage('Checkout Code') {
            steps {
                // Use powershell 'git clone' or just git if git plugin is installed
                checkout scm
            }
        }

        stage('Restore Dependencies') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --no-restore'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test --no-build --verbosity normal'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o out'
            }
        }
    }
}
