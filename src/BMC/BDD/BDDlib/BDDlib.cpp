// This is the main DLL file.
#pragma managed

#include "stdafx.h"

#include "BDDlib.h"
#include "Bdd.h"
#include "bdduser.h"


namespace BDDlib {

	BddManager::BddManager() 
	{
		m_Manager = bdd_init();
	}


	void BddManager::CheckUnmanedPointer()
	{
		if(m_Manager == nullptr) throw gcnew System::Exception("m_Manager is null pointer");
	}
	

	void BddManager::CheckBddInManager(Bdd ^f)
	{	
		CheckUnmanedPointer();
		if(m_Manager != f->UnmangedBddManager) throw gcnew System::Exception("BDDManager does not contain a given BDD");
	}


	System::String^ BddManager::Statistics() 
	{	
		System::String^ tempString = gcnew System::String(bdd_stats(m_Manager));
		return tempString;
	}


	BddManager::~BddManager() 
	{
		bdd_quit(m_Manager);
		System::Console::WriteLine("Destructor");
	}


	BddManager::!BddManager() 
	{
		System::Console::WriteLine("Finalizer");
		this->~BddManager();
	}


	Bdd^ BddManager::CreateBddVariableFirst() 
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_new_var_first(m_Manager);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::CreateBddVariableLast()
	{	
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_new_var_last(m_Manager);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::CreateBddVariableBefore(Bdd ^bddVariable)
	{	
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_new_var_before(m_Manager, bddVariable->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::CreateBddVariableAfter(Bdd ^bddVariable)
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_new_var_after(m_Manager, bddVariable->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::CreateBddOne()
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_one(m_Manager);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::CreateBddZero()
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_zero(m_Manager);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::LogicalAnd(Bdd ^f, Bdd ^g)
	{	
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_and(m_Manager, f->UnmanagedBdd, g->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::LogicalOr(Bdd ^f, Bdd ^g)
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_or(m_Manager, f->UnmanagedBdd, g->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::LogicalXor(Bdd ^f, Bdd ^g)
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_xor(m_Manager, f->UnmanagedBdd, g->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::LogicalNor(Bdd ^f, Bdd ^g)
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_nor(m_Manager, f->UnmanagedBdd, g->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::LogicalNand(Bdd ^f, Bdd ^g)
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_nand(m_Manager, f->UnmanagedBdd, g->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::LogicalNot(Bdd ^f)
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_not(m_Manager, f->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}

	Bdd^ BddManager::LogicalXnor(Bdd ^f, Bdd ^g)
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_xnor(m_Manager, f->UnmanagedBdd, g->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::LogicalIfThenElse(Bdd ^f, Bdd ^g, Bdd ^h)
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_ite(m_Manager, f->UnmanagedBdd, g->UnmanagedBdd, h->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}
	

	Bdd^ BddManager::LogicalImplies(Bdd ^f, Bdd ^g)
	{
		Bdd ^tempBdd =gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_or(m_Manager, bdd_not(m_Manager, f->UnmanagedBdd), g->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ BddManager::GetBddVariableWithIndex(long index) 
	{
		Bdd ^tempBdd = nullptr;
		bdd tempUnmanagedBdd;

		tempUnmanagedBdd = bdd_var_with_index(m_Manager, index);

		if(tempUnmanagedBdd != nullptr) 
		{
			tempBdd = gcnew Bdd();
			tempBdd->UnmanagedBdd = tempUnmanagedBdd;
			tempBdd->UnmangedBddManager = m_Manager;
		}

		return tempBdd;
	}


	Bdd^ BddManager::GetBddVariableWithID(long id) 
	{
		Bdd ^tempBdd = nullptr;
		bdd tempUnmanagedBdd;
		
		tempUnmanagedBdd = bdd_var_with_id(m_Manager, id);

		if(tempUnmanagedBdd != nullptr) 
		{
			tempBdd = gcnew Bdd();
			tempBdd->UnmanagedBdd = tempUnmanagedBdd;
			tempBdd->UnmangedBddManager = m_Manager;
		}

		return tempBdd;	
	}


	void BddManager::ForceGarbageCollection()
	{
		bdd_gc(m_Manager);
	}


	Bdd^ BddManager::Compose(Bdd ^f, Bdd ^g, Bdd ^h)
	{
		Bdd ^tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_compose(m_Manager, f->UnmanagedBdd, g->UnmanagedBdd, h->UnmanagedBdd);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	void BddManager::DeleteAll()
	{
		//bdd_clear_refs(m_Manager);  ????
		bdd_quit(m_Manager);
		m_Manager = bdd_init();
	}

}