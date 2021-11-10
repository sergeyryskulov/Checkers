﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class GetFiguresApiService
    {
        private BoardRepository _boardRepository;

        public GetFiguresApiService(BoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public string GetFigures(string userId)
        {
            return _boardRepository.Load(userId);
        }
    }
}
