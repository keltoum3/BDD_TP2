Feature: Election Result Calculation
As a client of the API at the end of a majority vote
I want to calculate the result of the vote
So that I can obtain the winner of the vote

	Scenario: un candidat a plus 50%
	Given election fini
	And il y a 3 candidat
	And candidat 1 a 59% des votes
	When je calcule le resultat de election
	Then candidat 1 est declare gagnant


	Scenario: pas de majorite absolu
	Given election fini
	And il y a 3 candidat
	And candidat 1 a 47% des votes
	And candidat 2 a 32% des votes
	And candidat 3 a 21% des votes
	When je calcule le resultat de election
	Then un second tour est necessaire
	And seulement les candidats 1 et 2 participeront au deuxieme tour

	Scenario: egalite
		Given second tour fini
		And il y a 2 candidat
		And les deux ont 50% de vote
		When je calcule le second tour
		Then pas de gagnant

	Scenario: le second tour donne un gagnant
		Given second tour fini
		And il y a 2 candidat
		And candidat 1 a 74% des votes
		And candidat 2 a 26% des votes
		When je calcule le second tour
		Then candidat 1 est declare gagnant 