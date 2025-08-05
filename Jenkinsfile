pipeline {
    agent any

    tools {
        // Uncomment and configure if you have .NET SDK configured in Jenkins
        // dotnet "dotnet6"
    }

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = "1"
    }

    stages {
        stage('Checkout Code') {
            steps {
                git credentialsId: 'your-credentials-id-if-needed', url: 'https://github.com/poojithanandam/ScrumStandUpTracker_1.git'
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

