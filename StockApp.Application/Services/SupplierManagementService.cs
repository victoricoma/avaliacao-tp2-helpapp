using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    /// <summary>
    /// Serviço para gestão de fornecedores
    /// </summary>
    public class SupplierManagementService : ISupplierManagementService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierContractRepository _contractRepository;
        private readonly ISupplierEvaluationRepository _evaluationRepository;
        private readonly IMapper _mapper;

        public SupplierManagementService(
            ISupplierRepository supplierRepository,
            ISupplierContractRepository contractRepository,
            ISupplierEvaluationRepository evaluationRepository,
            IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _contractRepository = contractRepository;
            _evaluationRepository = evaluationRepository;
            _mapper = mapper;
        }

        public async Task<SupplierDTO> AddSupplierAsync(CreateSupplierDto createSupplierDto)
        {
            // Mapear DTO para entidade
            var supplier = new Supplier(
                createSupplierDto.Name,
                createSupplierDto.ContactEmail,
                createSupplierDto.PhoneNumber);

            // Salvar no repositório
            var createdSupplier = await _supplierRepository.Create(supplier);

            // Mapear entidade para DTO de resposta
            var supplierDto = new SupplierDTO
            {
                Id = createdSupplier.Id,
                Name = createdSupplier.Name,
                ContactEmail = createdSupplier.ContactEmail,
                PhoneNumber = createdSupplier.PhoneNumber,
                EvaluationScore = null,
                HasActiveContract = false
            };

            return supplierDto;
        }

        public async Task<SupplierContractDto> ManageContractAsync(int supplierId, SupplierContractDto contractDto)
        {
            // Verificar se o fornecedor existe
            var supplier = await _supplierRepository.GetById(supplierId);
            if (supplier == null)
                throw new ApplicationException($"Fornecedor com ID {supplierId} não encontrado.");

            // Se o contrato já existe, atualiza
            if (contractDto.Id > 0)
            {
                var existingContract = await _contractRepository.GetByIdAsync(contractDto.Id);
                if (existingContract == null)
                    throw new ApplicationException($"Contrato com ID {contractDto.Id} não encontrado.");

                // Atualizar contrato existente
                existingContract.Update(
                    contractDto.Description,
                    contractDto.EndDate,
                    contractDto.Value,
                    contractDto.IsActive);

                await _contractRepository.UpdateAsync(existingContract);
                
                // Atualizar DTO com dados atualizados
                contractDto.UpdatedAt = DateTime.Now;
            }
            else
            {
                // Criar novo contrato
                var newContract = new SupplierContract(
                    contractDto.ContractNumber,
                    contractDto.Description,
                    contractDto.StartDate,
                    contractDto.EndDate,
                    contractDto.Value);

                // Definir o ID do fornecedor
                var contract = new SupplierContract(
                    0,
                    supplierId,
                    contractDto.ContractNumber,
                    contractDto.Description,
                    contractDto.StartDate,
                    contractDto.EndDate,
                    contractDto.Value,
                    contractDto.IsActive);

                var createdContract = await _contractRepository.CreateAsync(contract);
                
                // Atualizar DTO com ID gerado
                contractDto.Id = createdContract.Id;
                contractDto.CreatedAt = createdContract.CreatedAt;
            }

            return contractDto;
        }

        public async Task<SupplierEvaluationDto> EvaluateSupplierAsync(int supplierId, SupplierEvaluationDto evaluationDto)
        {
            // Verificar se o fornecedor existe
            var supplier = await _supplierRepository.GetById(supplierId);
            if (supplier == null)
                throw new ApplicationException($"Fornecedor com ID {supplierId} não encontrado.");

            // Criar nova avaliação
            var evaluation = new SupplierEvaluation(
                evaluationDto.Category,
                evaluationDto.Comment,
                evaluationDto.Score,
                evaluationDto.EvaluatedBy);

            // Definir o ID do fornecedor
            var supplierEvaluation = new SupplierEvaluation(
                0,
                supplierId,
                evaluationDto.Category,
                evaluationDto.Comment,
                evaluationDto.Score,
                evaluationDto.EvaluatedBy,
                DateTime.Now);

            var createdEvaluation = await _evaluationRepository.CreateAsync(supplierEvaluation);
            
            // Atualizar DTO com ID gerado
            evaluationDto.Id = createdEvaluation.Id;
            evaluationDto.SupplierId = supplierId;
            evaluationDto.EvaluationDate = createdEvaluation.EvaluationDate;

            return evaluationDto;
        }

        public async Task<IEnumerable<SupplierEvaluationDto>> GetSupplierEvaluationsAsync(int supplierId)
        {
            // Verificar se o fornecedor existe
            var supplier = await _supplierRepository.GetById(supplierId);
            if (supplier == null)
                throw new ApplicationException($"Fornecedor com ID {supplierId} não encontrado.");

            // Obter avaliações do fornecedor
            var evaluations = await _evaluationRepository.GetBySupplierId(supplierId);
            
            // Mapear para DTOs
            var evaluationDtos = new List<SupplierEvaluationDto>();
            foreach (var evaluation in evaluations)
            {
                evaluationDtos.Add(new SupplierEvaluationDto
                {
                    Id = evaluation.Id,
                    SupplierId = evaluation.SupplierId,
                    Category = evaluation.Category,
                    Comment = evaluation.Comment,
                    Score = evaluation.Score,
                    EvaluatedBy = evaluation.EvaluatedBy,
                    EvaluationDate = evaluation.EvaluationDate
                });
            }

            return evaluationDtos;
        }

        public async Task<IEnumerable<SupplierContractDto>> GetSupplierContractsAsync(int supplierId)
        {
            // Verificar se o fornecedor existe
            var supplier = await _supplierRepository.GetById(supplierId);
            if (supplier == null)
                throw new ApplicationException($"Fornecedor com ID {supplierId} não encontrado.");

            // Obter contratos do fornecedor
            var contracts = await _contractRepository.GetBySupplierId(supplierId);
            
            // Mapear para DTOs
            var contractDtos = new List<SupplierContractDto>();
            foreach (var contract in contracts)
            {
                contractDtos.Add(new SupplierContractDto
                {
                    Id = contract.Id,
                    SupplierId = contract.SupplierId,
                    ContractNumber = contract.ContractNumber,
                    Description = contract.Description,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    Value = contract.Value,
                    IsActive = contract.IsActive,
                    CreatedAt = contract.CreatedAt,
                    UpdatedAt = contract.UpdatedAt
                });
            }

            return contractDtos;
        }
    }
}