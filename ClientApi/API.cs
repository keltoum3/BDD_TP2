using System.Collections.Generic;
using System.Linq;

namespace ElectionResultCalculator
{
    public class ElectionResult
    {
        private List<Candidate> _candidates = new List<Candidate>();
        private List<int> _candidateVotes = new List<int>();

        public class Candidate
        {
            public string Name;
            public int Votes;
            public int Id;

            public Candidate(int id, string name, int votes)
            {
                Id = id;
                Name = name;
                Votes = votes;
            }
        }

        public List<int> CreateListCandidates(int nbCandidates)
        {
            for (int i = 1; i <= nbCandidates; i++)
            {
                var candidate = new Candidate(id: i, name: $"Candidate {i}", votes: 0);
                _candidates.Add(candidate);
                _candidateVotes.Add(0);
            }

            return _candidateVotes;
        }

        public void AddVotesForCandidate(int candidateNumber, int percentage)
        {
            var candidate = _candidates.SingleOrDefault(c => c.Id == candidateNumber);
            if (candidate != null)
            {
                candidate.Votes += percentage;
                _candidateVotes = _candidates.Select(c => c.Votes).ToList();
                _candidates = _candidates.OrderByDescending(c => c.Votes).ToList();
            }
            else
            {
                candidate.Votes += 0;
            }
        }

        public int CalculateElectionResult(List<int> nbVotesOfCandidates)
        {
            for (int i = 0; i < nbVotesOfCandidates.Count; i++)
            {
                _candidates[i].Votes = nbVotesOfCandidates[i];
            }

            return _candidates[0].Id;
        }

        public bool ShouldHoldSecondRound()
        {
            return ElectionResult.SecondRoundRequired(_candidateVotes);
        }

        public List<int> CandidatesInSecondRound()
        {
            List<int> candidates = new List<int>();
            List<int> sortedVotes = _candidateVotes.OrderByDescending(v => v).ToList();
            int maxVotes = sortedVotes[0];
            int secondMaxVotes = sortedVotes[1];

            for (int i = 0; i < _candidateVotes.Count; i++)
            {
                if (_candidateVotes[i] == maxVotes)
                {
                    candidates.Add(i + 1);
                }
                else if (_candidateVotes[i] == secondMaxVotes)
                {
                    candidates.Add(i + 1);
                }
            }

            return candidates;
        }


        public static bool SecondRoundRequired(List<int> votes)
        {
            int totalVotes = votes.Sum();
            int maxVotes = votes.Max();

            if (maxVotes > totalVotes / 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static List<int> GetCandidatesForSecondRound(List<int> votes)
        {
            List<int> candidates = new List<int>();
            int maxVotes = votes.Max();

            for (int i = 0; i < votes.Count; i++)
            {
                if (votes[i] == maxVotes)
                {
                    candidates.Add(i + 1);
                }
            }

            return candidates;
        }
    }
}
