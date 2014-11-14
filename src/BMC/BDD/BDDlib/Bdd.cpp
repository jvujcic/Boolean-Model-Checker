// Bdd.cpp
#pragma managed

#include "Bdd.h"

namespace BDDlib {

	Bdd::Bdd()
	{
		m_BDD = nullptr;
		m_Manager = nullptr;
	}
	

	void Bdd::CheckUnmanedPointer()
	{
		if(m_Manager == nullptr)
			throw gcnew System::Exception("m_Manager is null pointer");

		if(m_BDD == nullptr)
			throw gcnew System::Exception("m_BDD is null pointer");
	}


	System::String^ Bdd::ToString()
	{
		return "ID:" + (this->GetBddRootVariableID()).ToString() + "	" + "Index: " + (this->GetBddRootVariableIndex()).ToString();
	}


	void Bdd::FreeBdd()
	{
		bdd_free(m_Manager, m_BDD);
		//m_BDD = nullptr;
		//m_Manager = nullptr;
	}

	void Bdd::UnfreeBdd()
	{
		bdd_unfree(m_Manager, m_BDD);
	}


	BddType Bdd::ReturnBddType()
	{
		return (BddType)bdd_type(m_Manager, m_BDD);
	}


	long Bdd::GetBddRootVariableID()
	{
		return bdd_if_id(m_Manager, m_BDD);
	}


	long Bdd::GetBddRootVariableIndex()
	{
		return bdd_if_index(m_Manager, m_BDD);
	}

	
	Bdd^ Bdd::GetElseBranch() 
	{
		Bdd^ tempBdd = nullptr;
		tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_else(m_Manager, m_BDD);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ Bdd::GetThenBranch() 
	{
		Bdd^ tempBdd = nullptr;
		tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_then(m_Manager, m_BDD);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd^ Bdd::GetBddRootVariable()
	{
		Bdd^ tempBdd =nullptr;
		tempBdd = gcnew Bdd();
		tempBdd->UnmanagedBdd = bdd_if(m_Manager, m_BDD);
		tempBdd->UnmangedBddManager = m_Manager;
		return tempBdd;
	}


	Bdd ^Bdd::Exists(array<Bdd^> ^bddVariables)
	{
		Bdd^ tempBdd = gcnew Bdd();
		
		array<bdd> ^unmanagedBddArray = gcnew array<bdd>(bddVariables->Length+1);

		for(int i=0; i<bddVariables->Length; i++)
			unmanagedBddArray[i] = bddVariables[i]->UnmanagedBdd;

		unmanagedBddArray[bddVariables->Length] = nullptr;

		pin_ptr<bdd> pp = &unmanagedBddArray[0];

		bdd_temp_assoc(m_Manager, pp, 0);
		bdd_assoc(m_Manager, -1);

		tempBdd->UnmanagedBdd = bdd_exists(m_Manager, m_BDD);
		tempBdd->UnmangedBddManager = m_Manager;

		return tempBdd;
	}

	Bdd^ Bdd::Replace(array<Bdd^> ^bddVariablesToBdd)
	{
		Bdd ^tempBdd = gcnew Bdd();
		
		array<bdd> ^unmanagedBddArray = gcnew array<bdd>(bddVariablesToBdd->Length+1);

		for(int i=0; i<bddVariablesToBdd->Length; i++)
			unmanagedBddArray[i] = bddVariablesToBdd[i]->UnmanagedBdd;

		unmanagedBddArray[bddVariablesToBdd->Length] = nullptr;

		pin_ptr<bdd> pp = &unmanagedBddArray[0];

		bdd_temp_assoc(m_Manager, pp, 1);
		bdd_assoc(m_Manager, -1);

		tempBdd->UnmanagedBdd = bdd_substitute(m_Manager, m_BDD);
		tempBdd->UnmangedBddManager = m_Manager;

		return tempBdd;
	}
	

	int Bdd::UniqueKey()
	{
		return (int)m_BDD;
	}


	bool Bdd::EmptyBdd()
	{
		if(m_BDD == nullptr) return true;
		else return false;
	}

	int Bdd::RefCount()
	{
//		return BDD_REFS(m_BDD);
		return 1;
	}

}
