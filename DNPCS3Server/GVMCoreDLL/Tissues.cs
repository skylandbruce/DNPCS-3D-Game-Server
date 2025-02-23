
using DNPCS3DLL.GVM_UNIT.GVM_UNITGene;
using DNPCS3DLL.GVM_UNIT.GVM_UNITGenome;
using DNPCS3DLL.GVM_UNIT.GVM_UNITTissue;

namespace DNPCS3DLL.NS;

public class Tissues : Gene_D<Tissue_L>
{
    protected void RegistTissue(string Tag){
        Add(new Tissue_L(), Tag);
    }

    public void CultureTissue(Genome proto, int sizeOfBuff){
        if(proto is not Genome genome) return;

        RegistTissue(genome.Tag);
        Get(genome.Tag).Culture(proto, sizeOfBuff);
    }
}