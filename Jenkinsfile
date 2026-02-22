pipeline {
    agent any
    environment {
        DOCKER_HUB_USER = "thainv28"
        REGISTRY_CREDS  = "dockerhub-pass"
    }
    stages {
        stage('Checkout') {
            steps { checkout scm }
        }

       
}stage('Build & Push to Docker Hub') {
            steps {
                script {
                    docker.withRegistry('https://index.docker.io/v1/', REGISTRY_CREDS) {
                        
                        def backendImage = docker.build("${DOCKER_HUB_USER}/awakecup-backend:latest", "-f aspnetcore/Dockerfile .")
                        backendImage.push()

                        
                        def adminImage = docker.build("${DOCKER_HUB_USER}/awakecup-admin:latest", "./admin-react")
                        adminImage.push()

                        
                        def storeImage = docker.build("${DOCKER_HUB_USER}/awakecup-store:latest", "./store-react")
                        storeImage.push()
                    }
                }
            }
        }

        stage('Deploy on EC2') {
            steps {
                
                sh "DOCKER_HUB_USER=${DOCKER_HUB_USER} docker-compose pull"
                sh "DOCKER_HUB_USER=${DOCKER_HUB_USER} docker-compose up -d"
            }
        }
    }
    post {
        always {
            
            sh 'docker image prune -f'
        }
    }
}
