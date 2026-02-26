pipeline {
    agent any
    environment {
        DOCKER_HUB_USER = "thainv28"
        REGISTRY_CREDS  = "dockerhub-pass"
        DB_HOST = credentials('AWAKECUP_DB_HOST')
        DB_USER = credentials('AWAKECUP_DB_USER')
        DB_PASS = credentials('AWAKECUP_DB_PASS')
        DB_NAME = "awakecup"
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
                    withCredentials([usernamePassword(credentialsId: "${REGISTRY_CREDS}", passwordVariable: 'PASS', usernameVariable: 'USER')]) {
                        sh "echo \$PASS | docker login -u \$USER --password-stdin"
                        
                        sh "docker build -t ${DOCKER_HUB_USER}/awakecup-backend:latest -f aspnetcore/Dockerfile ."
                        sh "docker build -t ${DOCKER_HUB_USER}/awakecup-admin:latest ./admin-react"
                        sh "docker build -t ${DOCKER_HUB_USER}/awakecup-store:latest ./store-react"
                        
                        sh "docker push ${DOCKER_HUB_USER}/awakecup-backend:latest"
                        sh "docker push ${DOCKER_HUB_USER}/awakecup-admin:latest"
                        sh "docker push ${DOCKER_HUB_USER}/awakecup-store:latest"
                    }
                }
            }
        }

        stage('Deploy with RDS') {
            steps {
                script {
                    sh """
                        export DB_HOST=${DB_HOST}
                        export DB_USER=${DB_USER}
                        export DB_PASS=${DB_PASS}
                        export DB_NAME=${DB_NAME}
                        export DOCKER_HUB_USER=${DOCKER_HUB_USER}
                        
                        docker-compose down || true
                        docker-compose up -d --force-recreate
                    """
                    
                    echo "Initializing AWS RDS Database..."
                    sh """
                        mysql -h ${DB_HOST} -u ${DB_USER} -p${DB_PASS} -e "CREATE DATABASE IF NOT EXISTS ${DB_NAME};"
                        mysql -h ${DB_HOST} -u ${DB_USER} -p${DB_PASS} ${DB_NAME} < './database/table_data.sql'
                        mysql -h ${DB_HOST} -u ${DB_USER} -p${DB_PASS} ${DB_NAME} < './database/procedure_function.sql'
                    """
                }
            }
        }
    } 

    post {
        always {
            // clean disk
            sh "docker image prune -f"
        }
    }
} // Kết thúc khối PIPELINE
