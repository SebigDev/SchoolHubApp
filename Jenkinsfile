pipeline {
	agent: any
	stage('SCM-Checkout'){
		git credentialsId: 'git-credential', url: 'https://github.com/SebigDev/SchoolHubApp'
	}
	stage('Mvn Package'){
		sh label: '', script: 'mvn clean package'
	}
	stage('Build Docker Image'){
		sh 'docker build -t sebig230389/SchoolHubApp:1.0.0 .'
	}
	stage('Push Docker Image'){
		sh 'docker push -t sebig230389/SchoolHubApp:1.0.0'
	}
}
