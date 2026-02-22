pipeline {
    agent any
    environment {
        DOCKER_HUB_USER = "thainv28"
        REGISTRY_CREDS  = "dockerhub-pass"
    }
    stages {
        stage('Checkout Source') {
            steps {
                checkout scm
            }
        }

        stage('Build & Push Images') {
    steps {
        script {
            withCredentials([usernamePassword(credentialsId: 'dockerhub-pass', passwordVariable: 'PASS', usernameVariable: 'USER')]) {
                sh "echo \$PASS | docker login -u \$USER --password-stdin"
                
                
                sh "docker build -t thainv28/awakecup-backend:latest -f aspnetcore/Dockerfile ."
                sh "docker build -t thainv28/awakecup-admin:latest ./admin-react"
                sh "docker build -t thainv28/awakecup-store:latest ./store-react"
                
                
                sh "docker push thainv28/awakecup-backend:latest"
                sh "docker push thainv28/awakecup-admin:latest"
                sh "docker push thainv28/awakecup-store:latest"
            }
        }
    }
}

        stage('Deploy with Compose') {
            steps {
        
                sh "DOCKER_HUB_USER=${DOCKER_HUB_USER} docker-compose down || true"
                sh "DOCKER_HUB_USER=${DOCKER_HUB_USER} docker-compose up -d --force-recreate"
            }
        }

        stage('Init MySQL Database') {
            steps {
                script {
                    echo "Waiting for MySQL to start..."
                    sh "sleep 20" 
                    
                    sh """
                        docker exec -i awakecup-db mysql -u root -p'YourPassword123' -e "CREATE DATABASE IF NOT EXISTS awakecup;"
                        docker exec -i awakecup-db mysql -u root -p'YourPassword123' awakecup < ./database/table&data.sql
                        docker exec -i awakecup-db mysql -u root -p'YourPassword123' awakecup < ./database/procedure&function.sql
                    """
                }
            }
        }
    }
    post {
        always {
            sh "docker image prune -f"
        }
    }
}
