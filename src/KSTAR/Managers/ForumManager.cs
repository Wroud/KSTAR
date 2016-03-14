using KSTAR.Models;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.Managers
{
    public class ForumResult
    {
        public ForumResult(bool success)
        {
            Success = success;
        }
        public readonly bool Success;
    }
    public class ForumManager
    {
        public ForumManager(ApplicationDbContext context)
        {
            _context = context;
        }
        private ApplicationDbContext _context;

        public void Add<T>(T topic) where T : class
        {
            AddAsync(topic).GetAwaiter().GetResult();
        }
        public async Task AddAsync<T>(T topic) where T : class
        {
            _context.Add(topic);
            await _context.SaveChangesAsync();
        }

        public FGroup GetGroup(int id)
        {
            return _context.ForumGroup.Single(m => m.ID == id);
        }
        public FSubject GetSubject(int id)
        {
            return _context.ForumSubject.Single(s => s.ID == id);
        }
        public FTopic GetTopic(int id)
        {
            return _context.ForumTopic.Single(s => s.ID == id);
        }
        public List<FSubject> GetSubjectList()
        {
            return _context.ForumSubject.ToList();
        }
        public List<FGroup> GetGroupList()
        {
            return _context.ForumGroup.ToList();
        }
        public List<FTopic> GetTopicList()
        {
            return _context.ForumTopic.ToList();
        }
        public IEnumerable<FGroup> GetGroupWithSubjects()
        {
            return _context.ForumGroup.Include(g => g.Subjects);
        }
        public IEnumerable<FGroup> GetGroupWithSubjectsAndTopics()
        {
            return _context.ForumGroup.Include(g => g.Subjects).ThenInclude(s => s.Topics);
        }
        public IEnumerable<FGroup> GetGroupWithRelated()
        {
            return _context.ForumGroup.Include(g => g.Subjects).ThenInclude(s => s.Topics).ThenInclude(t => t.Post);
        }
        public IEnumerable<FSubject> GetSubjectWithTopics()
        {
            return _context.ForumSubject.Include(s => s.Group);
        }
        public IEnumerable<FTopic> GetTopicWithRelated()
        {
            return _context.ForumTopic.Include(t => t.Subject).Include(t => t.User).Include(t => t.Post);
        }

        public void Update<T>(T group) where T : class
        {
            UpdateAsync(group).GetAwaiter().GetResult();
        }
        public async Task UpdateAsync<T>(T group) where T : class
        {
            _context.Update(group);
            await _context.SaveChangesAsync();
        }

        public void DeleteGroup(int id)
        {
            DeleteGroupAsync(GetGroup(id)).GetAwaiter().GetResult();
        }
        public void DeleteGroup(FGroup group)
        {
            DeleteGroupAsync(group).GetAwaiter().GetResult();
        }
        public async Task DeleteGroupAsync(int id)
        {
            await DeleteGroupAsync(GetGroup(id));
        }
        public async Task DeleteGroupAsync(FGroup group)
        {
            _context.Remove(group);
            await _context.SaveChangesAsync();
        }
        public void DeleteSubject(int id)
        {
            DeleteSubjectAsync(GetSubject(id)).GetAwaiter().GetResult();
        }
        public void DeleteSubject(FSubject subject)
        {
            DeleteSubjectAsync(subject).GetAwaiter().GetResult();
        }
        public async Task DeleteSubjectAsync(int id)
        {
            await DeleteSubjectAsync(GetSubject(id));
        }
        public async Task DeleteSubjectAsync(FSubject subject)
        {
            _context.Remove(subject);
            await _context.SaveChangesAsync();
        }
        public void DeleteTopic(int id)
        {
            DeleteTopicAsync(GetTopic(id)).GetAwaiter().GetResult();
        }
        public void DeleteTopic(FTopic topic)
        {
            DeleteTopicAsync(topic).GetAwaiter().GetResult();
        }
        public async Task DeleteTopicAsync(int id)
        {
            await DeleteTopicAsync(GetTopic(id));
        }
        public async Task DeleteTopicAsync(FTopic topic)
        {
            _context.Remove(topic);
            await _context.SaveChangesAsync();
        }
    }
}
