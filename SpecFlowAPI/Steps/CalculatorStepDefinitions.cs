using System;
using System.Collections;
using TechTalk.SpecFlow;
using System.Reflection.Metadata.Ecma335;
using FluentAssertions;
using ElectionResultCalculator;

namespace ElectionResultCalculation.Specs
{
    [Binding]
    
    public class ElectionResultCalculationSteps
    {

        private List<int> CandidatesvOTES;
        private  int winner;
        private List<ElectionResult.Candidate> _candidates;
        private bool _electionClosed;
        private int _candidateNumber;
        private int _percentage;
        ElectionResult electionResult = new ElectionResult();

        [Given(@"election fini")]
        public void GivenTheElectionHasClosed()
        {
            _electionClosed = true;
        }
        [Given(@"il y a (.*) candidat")]
        public void GivenThereAreCandidates(int nbCandidates)
        {
            if (_electionClosed==true)
            {
                CandidatesvOTES = electionResult.CreateListCandidates(nbCandidates);
            } 
        }
        [Given(@"candidat (.*) a (.*)% des votes")]
        public void GivenCandidateHasOfTheVotes(int candidateNumber, int percentage)
        {
            _candidateNumber = candidateNumber;
            _percentage = percentage;
            electionResult.AddVotesForCandidate(_candidateNumber, _percentage);
        }
        [Given(@"second tour fini")]
        public void GivenTheSecondRoundOfElectionHasClosed()
        {
            _electionClosed = true;
        }

        [Given(@"les deux ont (.*)% de vote")]
        public void GivenBothCandidatesHaveOfTheVotesEach(int percentage)
        {
            electionResult.CreateListCandidates(2);
            electionResult.AddVotesForCandidate(1, percentage);
            electionResult.AddVotesForCandidate(2, percentage);
        } 


        [When(@"je calcule le resultat de election")]
        public void WhenICalculateTheResultOfTheElection()
        {
            winner= electionResult.CalculateElectionResult(CandidatesvOTES);
        }
        
        [When(@"je calcule le second tour")]
        public void WhenICalculateTheResultOfTheSecondRoundOfElection()
        {
            winner = electionResult.CalculateElectionResult(CandidatesvOTES);

        }

        [Then(@"candidat (.*) est declare gagnant")]
        public void ThenCandidateShouldBeDeclaredTheWinner(int candidate)
        {
            winner.Should().Be(candidate); 
        }
        [Then(@"un second tour est necessaire")]
        public void ThenASecondRoundOfElectionShouldBeHeld()
        {
            electionResult.ShouldHoldSecondRound().Should().BeTrue();
        }

        [Then(@"seulement les candidats (.*) et (.*) participeront au deuxieme tour")]
        public void ThenOnlyCandidateAndShouldParticipateInTheSecondRound(int candidate1, int candidate2)
        {
            electionResult.CandidatesInSecondRound().Should().BeEquivalentTo(new List<int> { candidate1, candidate2 });
            electionResult.CandidatesInSecondRound().Count.Should().Be(2);
        }
        [Then(@"pas de gagnant")]
        public void ThenNoWinnerCanBeDetermined()
        {
            winner.Should().Be(0);
        }
    }


}
